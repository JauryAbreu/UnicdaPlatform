/**
 * @file jquery.translate.js
 * @brief jQuery plugin to translate text in the client side.
 * @author Manuel Fernandes
 * @site
 * @version 0.9
 * @license MIT license <http://www.opensource.org/licenses/MIT>
 *
 * translate.js is a jQuery plugin to translate text in the client side.
 *
 */

(function($){
  $.fn.translate = function(options) {

    var that = this; // a reference to ourselves
	
    var settings = {
      css: "trn",
      lang: "en"
    };
    settings = $.extend(settings, options || {});
    if (settings.css.lastIndexOf(".", 0) !== 0)   // doesn't start with '.'
      settings.css = "." + settings.css;
       
    var translate = settings.t;
 
    // public methods
    this.lang = function(lang) {
      if (lang) {
        settings.lang = lang;
        this.translate(settings);  // translate everything
      }
        
      return settings.lang;
    };

    this.get = function(index) {
      var response = index;

      try {
        response = translate[index][settings.lang];
      }
      catch (err) {
        // not found, return index
        return index;
      }
      
      if (response)
        return response;
      else
        return index;
    };

    this.g = this.get;

    // main
    this.find(settings.css).each(function(i) {
      var $this = $(this);

      var trnKey = $this.attr("data-trn-key");
      if (!trnKey) {
          trnKey = $this.html();
        $this.attr("data-trn-key", trnKey);   // store key for next time
      }

      $this.html(that.get(trnKey));
    });
    
		return this;
  };
})(jQuery);