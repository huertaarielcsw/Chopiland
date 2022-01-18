(function ($) {  
 function Product() {  
 var $this = this;  
      
 function initilizeModel() {  
 $('#modal1-action-product').on('loaded.bs.modal', function (e) {  
     $('.js-modal1').addClass('show-modal1');
 }).on('click', function (e) {  
 $(this).removeData('bs.modal');  
 $('.js-modal1').removeClass('show-modal1');
 });  
 }  
 $this.init = function () {  
 initilizeModel();  
 }  
 }  
 $(function () {  
 var self = new Product();  
 self.init();  
 })  
 }(jQuery))  