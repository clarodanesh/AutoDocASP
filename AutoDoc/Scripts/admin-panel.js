$( document ).ready(function() {
    console.log( "ready!" );
    
    $('#admin-side-panel').css('height', window.innerHeight);
    $('#main-content').css('height', window.innerHeight);
    
    
    $('#admin-frame').css('height', GetFrameHeight());
    
    $( window ).resize(function() {
        $('#admin-side-panel').css('height', window.innerHeight);
        $('#main-content').css('height', window.innerHeight);
        $('#admin-frame').css('height', GetFrameHeight());
    });
});

function GetFrameHeight(){
    var headerHeight, actionBarHeight, containerHeight, frameHeight, tmpHeight;

    headerHeight = $('#main-header').height();
    actionBarHeight = $('#admin-action-container').height();
    containerHeight = $('#main-content').height();
    tmpHeight = headerHeight + actionBarHeight;
    frameHeight = containerHeight - tmpHeight;
    
    return frameHeight - 20;
}