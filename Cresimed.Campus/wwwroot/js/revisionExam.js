
function showQuestion(n) {
	$('.QuestionRevisions').hide();
	$('#QuestionRevision_'+(n-1)).show();
	$("html, body").animate({ scrollTop: 0 }, "smooth");


	

}

$(document).ready(function () {


	$('.QuestionRevisions').hide();
	

});

