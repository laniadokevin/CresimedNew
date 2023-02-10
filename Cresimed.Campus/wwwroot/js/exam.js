var currentTab = 0; // Current tab is set to be the first tab (0)
var secondClick = false;
var hor = 00;
var min = 00;
var sec = 00;
var timer;
var timerPorQuest;
var timeLimitExam;


function preventAllInteraction() {


	$(function () {
		$(this).bind("contextmenu", function (e) {
			e.preventDefault();
		});
	});
	/** TO DISABLE CUT COPY OR PASTE **/
	$(document).ready(function () {
		$('body').bind('cut copy paste', function (e) {
			e.preventDefault();
		});
		});

	/** TO DISABLE SCREEN CAPTURE **/
	document.addEventListener('keyup', (e) => {
				if (e.key == 'PrintScreen') {
		navigator.clipboard.writeText('');
				}
			});

	/** TO DISABLE PRINTS WHIT CTRL+P **/
	document.addEventListener('keydown', (e) => {
		if (e.ctrlKey && e.key == 'p') {
			e.cancelBubble = true;
			e.preventDefault();
			e.stopImmediatePropagation();
		}
	});
}

function fixStepIndicator(n) {

	// This function removes the "active" class of all steps...
	var i, x = document.getElementsByClassName("step");
	var corr = false;
	for (i = 0; i < x.length; i++) {
		x[n].className = x[n].className.replace(" finish", "");
		x[i].className = x[i].className.replace(" active", "");

		if (n > 0) {
			if (tutorActive) {

				$("#AnswersBox_" + (n - 1) + " :input").each(function (index) {

					var ischecked = $(this).is(':checked');
					var iscorrect = $(this).attr('iscorrect').toLowerCase() === "true";
					if (ischecked) {
						if (iscorrect)
							corr = true;
					}

				});

				if (corr)
					x[(n - 1)].className += " stepCorrect";
				else
					x[(n - 1)].className += " stepIncorrect";
			} else
				x[(n - 1)].className += " finish";
		}

	}
	//... and adds the "active" class on the current step:
	x[n].className += " active";

}

function showTab(n) {

	// This function will display the specified tab of the form...
	var x = document.getElementsByClassName("tab");
	x[n].style.display = "block";
	//... and fix the Previous/Next buttons:
	if (n == 0) {
		document.getElementById("prevBtn").style.display = "none";
	} else {
		document.getElementById("prevBtn").style.display = "block";
	}
	if (n == (x.length - 1)) {
		document.getElementById("nextBtn").innerHTML = "Terminar";
	} else {
		document.getElementById("nextBtn").innerHTML = "<i class='tf-icon bx bx-chevron-right'></i>";
	}
	//... and run a function that will display the correct step indicator:
	fixStepIndicator(n);
	clearInterval(timerPorQuest);
	startQuestionTimer();
}

function validateForm() {
	return true;
}

function hideShowSteps() {

	var actualInd = parseInt($(".step.active").first().attr("actual"));

	if (actualInd < 3)
		actualInd = 3;
	var elems = document.getElementsByClassName('step');

	if (actualInd > elems.length - 3)
		actualInd = elems.length - 3;
	

	$(".step").hide();

	for (var i = actualInd - 3; i < actualInd + 3; i++) {
		if (elems.item(i) != null) 
			elems.item(i).removeAttribute("style");
	}

}

function nextPrev(n) {
	hideShowSteps();
	if (tutorActive && !secondClick && n != -1) {
		showTutorMode();
		return;
	} else {

		// This function will figure out which tab to display
		var x = document.getElementsByClassName("tab");

		// Exit the function if any field in the current tab is invalid:
		if (n == 1 && !validateForm()) return false;

		// Hide the current tab:
		x[currentTab].style.display = "none";

		// Increase or decrease the current tab by 1:
		currentTab = currentTab + n;

		// if you have reached the end of the form...
		if (currentTab >= x.length) {

			// ... the form gets submitted:
			SubmitExam();

			return false;
		}
		if (tutorActive && n != -1) {
			var elementoDis = $("#AnswersBox_" + currentTab + " :input").first().prop("disabled");
			if (!elementoDis) {
				$(".ExplanationSection").hide();

			}
			startTimer();
			secondClick = false;

		}
		// Otherwise, display the correct tab:
		showTab(currentTab);
		if (tutorActive && n == -1)
		{
			showTutorMode();
			clearInterval(timerPorQuest);
        }


	}

}

function SubmitExam() {
	$('input').prop('disabled', false);
	document.getElementById("FullForm").submit();

}

function startTimer() {

	timer = setInterval(function () {

		sec++;

		var timeString = String(hor).padStart(2, '0') + ':' + String(min).padStart(2, '0') + ':' + String(sec).padStart(2, '0');
		$('.safeTimerDisplay').html(timeString);


		if (sec == 60) {
			min++;
			sec = 00;
		}
		if (min == 60) {
			hor++;
			min = 00;
		}

		var a = timeString.split(':'); // split it at the colons
		var b = timeLimitExam.split(':'); // split it at the colons

		// minutes are worth 60 seconds. Hours are worth 60 minutes.
		var seconds = (+a[0]) * 60 * 60 + (+a[1]) * 60 + (+a[2]);
		var secondsLimit = (+b[0]) * 60 * 60 + (+b[1]) * 60 + (+b[2]);

		if (seconds === secondsLimit) {
			$("#TimeUpear").click();
			stopTimer();
		}
	}, 1000);
}

function showTutorMode() {
	$(".ExplanationSection").show();
	stopTimer();
	showKeyWords();
	secondClick = true;
	clearInterval(timerPorQuest);
	fillCorrectionSection();
	$("#AnswersBox_" + currentTab + " :input").attr("disabled", true);
}

function fillCorrectionSection() {
	 
	var rtas = $('input[name="[' + currentTab + '].AnswerID"]');
	var optElegida;
	var optCorrecta;//opcion correcta

	for (i = 0; i < rtas.length; i++) {
		if (rtas[i].checked) {
			optElegida = rtas[i].getAttribute("NroOpcion");//opcion elegida por el usuario
			$("#RtaCorrecta_" + currentTab).text(optElegida);
		}
		if (rtas[i].getAttribute("iscorrect").toLowerCase() === "true") {
			optCorrecta = rtas[i].getAttribute("NroOpcion");//opcion elegida por el usuario
		}
	}

	var tiempoPregunta = $("#TiempoPersonal_" + currentTab).text();//demora del usuario en responder

	$("#TiempoPropio_" + currentTab).text(tiempoPregunta);

	if (optCorrecta == optElegida) {
		$("#TituloCorrectONo_" + currentTab).text("Correcta");
		$("#TituloCorrectONo_" + currentTab).css("color", "green");
		$("#BarritaCorrecta_" + currentTab).css("border-left","5px solid green");
	} else
	{
		$("#TituloCorrectONo_" + currentTab).text("Incorrecta");
		$("#TituloCorrectONo_" + currentTab).css("color","red");
		$("#BarritaCorrecta_" + currentTab).css("border-left","5px solid red");
	}
	
}

function stopTimer() {
	clearInterval(timer);
	timer = null;
}

function showKeyWords() {

	var countkw = $("#CurrentCountKW" + currentTab).val();
	var oldText = $("#CurrentQuestionText_" + currentTab).text();
	for (var i = 0; i < countkw; i++) {

		var kw = $("#CurrentKeyWords_" + currentTab + i).val();

		var markedText = "<span  style='white-space: nowrap !important;background-color:#aeebd3;font-weight:700;'>" + kw + "</span>";

		oldText = oldText.replace(kw, markedText);

	}
	$("#CurrentQuestionText_" + currentTab).empty().append(oldText);


}

function ConfirmDialog() {
	$("#FullForm").submit();

};

function startQuestionTimer() {
	timerPorQuest = setInterval(function () {

		var	segundos = parseInt($("#sec_" + currentTab).text());
		var	minutos = parseInt($("#min_" + currentTab).text());
		var horas = parseInt($("#hor_" + currentTab).text());

		segundos++;

		if (segundos == 60) {
			minutos++;
			segundos = 00;
		}
		if (minutos == 60) {
			horas++;
			minutos = 00;
		}

		$("#hor_" + currentTab).empty();
		$("#min_" + currentTab).empty();
		$("#sec_" + currentTab).empty();
		$("#hor_" + currentTab).append(String(horas).padStart(2, '0'));
		$("#min_" + currentTab).append(String(minutos).padStart(2, '0'));
		$("#sec_" + currentTab).append(String(segundos).padStart(2, '0'));
		;
		;
		;
		var hms = String(horas).padStart(2, '0') + ':' + String(minutos).padStart(2, '0') + ':' + String(segundos).padStart(2, '0');
		var a = hms.split(':'); // split it at the colons

		// minutes are worth 60 seconds. Hours are worth 60 minutes.
		var seconds = (+a[0]) * 60 * 60 + (+a[1]) * 60 + (+a[2]);


		$("#CurrentTime_" + currentTab).val(seconds);

	}, 1000);
};

$('#myModal').on('shown.bs.modal', function () {
		$('#myInput').trigger('focus')
	});

$("#RptSubmitForm").on('click', function () {
	var rptQuestionID = $('#RptQuestionID').val();
	var rptUserID = $('#RptUserID').val();
	var rptType = $('#RptType').val();
	var rptMessage = $('#RptMessage').val();
	var questionID = $("#CurrentQuestionID" + currentTab);

	var data = { 'Type': parseInt(rptType), 'QuestionID': parseInt(questionID.val()), 'UserID': parseInt(rptUserID), 'Message': rptMessage };

	$.ajax({
		url: window.location.origin + '/Exam/NewReport',
		type: "POST",
		data: data,
		success: function (data, textStatus, jqXHR) {
			$('#RptMessage').val("");
			alert(data);
		},
		error: function (jqXHR, textStatus, errorThrown) {
			alert("Mal");

		}
	});
});

$(document).ready(function () {

	startTimer();

	$(".textoPreguntas").each(function () {
		$(this).text($(this).text().replace("                                    ", ""));
	});
	//preventAllInteraction(); ACTIVAR PARA PRODUCCION

	timeLimitExam = $("#TimeLimitExam").text();
	$(".ExplanationSection").hide();
	$("#template-customizer").hide();
	
	//$(".step").on("click", function () {
	//	showTab(this.innerHTML);
	//})

	showTab(currentTab); // Display the current tab
	hideShowSteps();

});

