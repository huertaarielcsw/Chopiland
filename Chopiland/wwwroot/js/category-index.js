(function ($) {  
 function Category() {  
 var $this = this;  
      
 function initilizeModel() {  
 $('#modal1-action-category').on('loaded.bs.modal', function (e) {  
     $('.js-modal1').addClass('show-modal1');
 }).on('hide.bs.modal', function (e) {  
 $(this).removeData('bs.modal');  
 $('.js-modal1').removeClass('show-modal1');
 });  
 }  
 $this.init = function () {  
 initilizeModel();  
 }  
 }  
 $(function () {  
 var self = new Category();  
 self.init();  
 })  
 }(jQuery))  