	var currentTab = 0; // Current tab is set to be the first tab (0)
	var secondClick = false;
	var hor = 00;
	var min = 00;
	var sec = 00;
	var timer;

	showTab(currentTab); // Display the current tab


	function fixStepIndicator(n) {

		// This function removes the "active" class of all steps...
		var i, x = document.getElementsByClassName("step");
		for (i = 0; i < x.length; i++) {
			x[n].className = x[n].className.replace(" finish", "");
			x[i].className = x[i].className.replace(" active", "");
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
			document.getElementById("prevBtn").style.display = "inline";
		}
		if (n == (x.length - 1)) {
			document.getElementById("nextBtn").innerHTML = "Finish";
		} else {
			document.getElementById("nextBtn").innerHTML = "<i class='fa fa-arrow-right' aria-hidden='true'></i>";
		}
		//... and run a function that will display the correct step indicator:
		fixStepIndicator(n)
	}

	function validateForm() {
		// This function deals with validation of the form fields
		var x, y, i, valid = true;
		x = document.getElementsByClassName("tab");
		y = x[currentTab].getElementsByTagName("input");
		// A loop that checks every input field in the current tab:
		for (i = 0; i < y.length; i++) {
			// If a field is empty...
			if (y[i].value == "") {
				// add an "invalid" class to the field:
				y[i].className += " invalid";
				// and set the current valid status to false
				valid = false;
			}
		}
		// If the valid status is true, mark the step as finished and valid:
		if (valid) {
			document.getElementsByClassName("step")[currentTab].className += " finish";
		}
		return valid; // return the valid status
	}

	function nextPrev(n) {
		if (tutorActive && !secondClick) {
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
				document.getElementById("regForm").submit();
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


		}

	}

	function startTimer() {

		timer = setInterval(function () {

			var timeString = String(hor).padStart(2, '0') + ':' + String(min).padStart(2, '0') + ':' + String(sec).padStart(2, '0');
			$('.safeTimerDisplay').html(timeString);

			sec++;

			if (sec == 60) {
				min++;
				sec = 00;
			}
			if (min == 60) {
				hor++;
				min = 00;
			}
		}, 1000);
	}

	function showTutorMode() {
		$(".ExplanationSection").show();
		stopTimer();
		secondClick = true;
		$("#AnswersBox_" + currentTab +" :input").attr("disabled", true);
	}

	function stopTimer() {
		clearInterval(timer);
	}

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

	$(function () {
		$(this).bind("contextmenu", function (e) {
			e.preventDefault();
		});
	});

	$('#myModal').on('shown.bs.modal', function () {
		$('#myInput').trigger('focus')
	});


	//$(document).ready()
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
		$(".ExplanationSection").hide();
	});

