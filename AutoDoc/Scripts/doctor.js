$(document).ready(function () {
    console.log("ready!");

    $('#admin-frame').css('height', GetFrameHeight());

    $(window).resize(function () {
        $('#admin-frame').css('height', GetFrameHeight());
    });
});

function GetFrameHeight() {
    var headerHeight, actionBarHeight, containerHeight, frameHeight, tmpHeight;

    headerHeight = $('#doc-title').height();
    actionBarHeight = $('#footer').height();
    tmpHeight = headerHeight + actionBarHeight;
    frameHeight = window.innerHeight - tmpHeight;

    return frameHeight - 30;
}