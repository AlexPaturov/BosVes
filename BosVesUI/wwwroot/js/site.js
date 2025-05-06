function savePdfToIndexedDB(pdfBlob, pdfName) {
    const request = indexedDB.open("PdfDatabase", 1);

    request.onupgradeneeded = function (event) {
        const db = event.target.result;
        if (!db.objectStoreNames.contains("pdfs")) {
            db.createObjectStore("pdfs", { keyPath: "name" });
        }
    };

    request.onsuccess = function (event) {
        const db = event.target.result;
        const transaction = db.transaction("pdfs", "readwrite");
        const store = transaction.objectStore("pdfs");

        const pdfData = { name: pdfName, blob: pdfBlob };
        const addRequest = store.add(pdfData);

        addRequest.onsuccess = function () {
            console.log("PDF saved successfully!");
        };

        addRequest.onerror = function (error) {
            console.error("Error saving PDF:", error);
        };
    };

    request.onerror = function (error) {
        console.error("Error opening IndexedDB:", error);
    };
}

// Получение PDF из IndexedDB
function getPdfFromIndexedDB(pdfName, dotNetHelper) {
    const request = indexedDB.open("PdfDatabase", 1);

    request.onsuccess = function (event) {
        const db = event.target.result;
        const transaction = db.transaction("pdfs", "readonly");
        const store = transaction.objectStore("pdfs");
        const getRequest = store.get(pdfName);

        getRequest.onsuccess = function () {
            if (getRequest.result) {
                // Передаем blob обратно в C#
                dotNetHelper.invokeMethodAsync("getPdfFromIndexedDB", getRequest.result.blob);
            } else {
                console.error(`PDF ${pdfName} не найден в IndexedDB.`);
            }
        };

        getRequest.onerror = function (error) {
            console.error("Ошибка при получении из IndexedDB:", error);
        };
    };

    request.onerror = function (error) {
        console.error("Ошибка при открытии IndexedDB:", error);
    };
}

window.openPdfForPrint = function (base64Pdf) {
    const byteCharacters = atob(base64Pdf.split(',')[1]);
    const byteNumbers = new Array(byteCharacters.length).fill().map((_, i) => byteCharacters.charCodeAt(i));
    const byteArray = new Uint8Array(byteNumbers);

    const blob = new Blob([byteArray], { type: 'application/pdf' });
    const blobUrl = URL.createObjectURL(blob);

    const iframe = document.createElement('iframe');
    iframe.style.display = 'none';
    iframe.src = blobUrl;
    document.body.appendChild(iframe);

    iframe.contentWindow.focus();
    iframe.contentWindow.print();
    document.body.removeChild(iframe);
}

window.createPdfBlob = (pdfBytes) => {
    const blob = new Blob([new Uint8Array(pdfBytes)], { type: "application/pdf" });
    const blobUrl = URL.createObjectURL(blob);

    const iframe = document.getElementById("pdfFrame");
    if (iframe) {
        iframe.src = blobUrl;

        iframe.onload = () =>
        {
            iframe.contentWindow.focus();
        }
    }
}
