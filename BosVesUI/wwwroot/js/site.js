﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// размер окна 
window.getWindowSize = () => {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};

// для логирования закрытия окна окна 
window.onbeforeunload = function () {
    DotNet.invokeMethodAsync('BosVesUI', 'LogWindowClose');
};

function getWindowWidth() {
    return window.innerWidth;
}


