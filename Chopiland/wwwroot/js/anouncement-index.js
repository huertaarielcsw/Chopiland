(function ($) {  
 function Anouncement() {  
 var $this = this;  
      
 function initilizeModel() {  
 $('#modal-action-anouncement').on('loaded.bs.modal', function (e) {  

 }).on('hidden.bs.modal', function (e) {  
 $(this).removeData('bs.modal');  
 });
 }  
 $this.init = function () {  
 initilizeModel();  
 }  
 }  
 $(function () {  
 var self = new Anouncement();  
 self.init();  
 })  
 }(jQuery))  