// перехват события на input компоненте
window.blazorKeyPress = {
    addF1KeyListener: function (element, dotNetHelper) {
        element.addEventListener('keydown', function (event) {
            if (event.key === 'F1') {
                event.preventDefault(); // Prevent the default F1 help behavior
                dotNetHelper.invokeMethodAsync('OnF1KeyPressed');
            }
        });
    }
};