"use strict";
// HAMBURGLERv2

function togglescroll() {
  $('body').on('touchstart', function(e) {
    if ($('body').hasClass('noscroll')) {
      e.preventDefault();
    }
  });
}

// PUSH ESC KEY TO EXIT
$(document).keydown(function(e) {
  if (e.keyCode == 27) {
    $(".mobilenav").fadeOut(500);
    $(".top-menu").removeClass("top-animate");
    $("body").removeClass("noscroll");
    $(".mid-menu").removeClass("mid-animate");
    $(".bottom-menu").removeClass("bottom-animate");
  }
});

$(document).ready(function() {
    var attachFastClick = Origami.fastclick;
    attachFastClick(document.body);
    
    $('article img').featherlight({
        targetAttr: 'src'
    });
        
    togglescroll();
    $(".icon").click(function() {
        $(".mobilenav").fadeToggle(500);
        $(".top-menu").toggleClass("top-animate");
        $("body").toggleClass("noscroll");
        $(".mid-menu").toggleClass("mid-animate");
        $(".bottom-menu").toggleClass("bottom-animate");
        $(".icon").toggleClass('expand');
    });
      
     
    $(window).resize(function() {
      var wasVisible = $(".icon").hasClass('expand');
      if ($(window).width() > 1000) {            
            $(".mobilenav").show();
            $(".top-menu").removeClass("top-animate");
            $("body").removeClass("noscroll");
            $(".mid-menu").removeClass("mid-animate");
            $(".bottom-menu").removeClass("bottom-animate");
      } else {
          if(wasVisible) {
            $(".mobilenav").show();
            $(".top-menu").addClass("top-animate");
            $("body").addClass("noscroll");
            $(".mid-menu").addClass("mid-animate");
            $(".bottom-menu").addClass("bottom-animate");
          }
      }
    });        
}); 