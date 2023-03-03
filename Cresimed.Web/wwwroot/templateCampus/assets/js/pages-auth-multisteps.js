/**
 *  Page auth register multi-steps
 */

'use strict';

// Select2 (jquery)
$(function () {
    var select2 = $('.select2');

    // select2
    if (select2.length) {
        select2.each(function () {
            var $this = $(this);
            $this.wrap('<div class="position-relative"></div>');
            $this.select2({
                placeholder: 'Select a country',
                dropdownParent: $this.parent()
            });
        });
    }
});

// Multi Steps Validation
// --------------------------------------------------------------------
document.addEventListener('DOMContentLoaded', function (e) {
    (function () {
        const stepsValidation = document.querySelector('#multiStepsValidation');
        if (typeof stepsValidation !== undefined && stepsValidation !== null) {
            // Multi Steps form
            const stepsValidationForm = stepsValidation.querySelector('#multiStepsForm');
            // Form steps
            const stepsValidationFormStep1 = stepsValidationForm.querySelector('#accountDetailsValidation');
            const stepsValidationFormStep2 = stepsValidationForm.querySelector('#personalInfoValidation');
            const stepsValidationFormStep3 = stepsValidationForm.querySelector('#billingLinksValidation');
            // Multi steps next prev button
            const stepsValidationNext = [].slice.call(stepsValidationForm.querySelectorAll('.btn-next'));
            const stepsValidationPrev = [].slice.call(stepsValidationForm.querySelectorAll('.btn-prev'));

            const vacio = "";
            var hecho = true;

            function resetStep1() {

                $("#multiStepsUsernameMsg").text(vacio);
                $("#multiStepsUsername").css("border", "none");

                $("#multiStepsEmailMsg").text(vacio);
                $("#multiStepsEmail").css("border", "none");

                $("#multiStepsPassMsg").text(vacio);
                $("#multiStepsPass").css("border", "none");

                $("#multiStepsConfirmPassMsg").text(vacio);
                $("#multiStepsConfirmPass").css("border", "none");



            }
            function resetStep2() {

                $("#multiStepsFirstNameMsg").text(vacio)
                $("#multiStepsFirstName").css("border", "none");

                
                $("#multiStepsLastNameMsg").text(vacio)                
                $("#multiStepsLastName").css("border", "none");

                
                $("#multiStepsCountryMsg").text(vacio)                
                $("#multiStepsCountry").css("border", "none");

                
                $("#multiStepsProvinceMsg").text(vacio)                
                $("#multiStepsProvince").css("border", "none");


            }
            function resetStep3() {


                $("#multiStepsUniversityMsg").text(vacio)
                $("#multiStepsUniversity").css("border", "none");


                $("#multiStepsLastYearMsg").text(vacio)
                $("#multiStepsLastYear").css("border", "none");

            }

            function validateStep1() {
                resetStep1();
                var valid = true;

                if ($("#multiStepsUsername").val().trim() == vacio) {

                    $("#multiStepsUsernameMsg").text("Debe completar este campo");
                    $("#multiStepsUsername").css("border", "solid 1px red");
                    valid = false;

                }

                console.log($("#multiStepsUsernameMsg").text());
                if ($("#multiStepsUsernameMsg").text().trim() == "Ese nombre de usuario ya est\xE1 en uso. Prueba con otro.".trim()) {
                    valid = false;

                }


                if ($("#multiStepsEmail").val() == vacio) {

                    $("#multiStepsEmailMsg").text("Debe completar este campo");
                    $("#multiStepsEmail").css("border", "solid 1px red");
                    valid = false;

                } else if (!isEmail($("#multiStepsEmail").val())) {

                    $("#multiStepsEmailMsg").text("El formato del Email no es correcto");
                    $("#multiStepsEmail").css("border", "solid 1px red");
                    valid = false;
                }


                    console.log($("#multiStepsEmailMsg").text());
                if ($("#multiStepsEmailMsg").text() == "Ese email ya est\xE1 en uso. Prueba con otro.") {

                    valid = false;

                }

                if ($("#multiStepsPass").val() == vacio) {

                    $("#multiStepsPassMsg").text("Debe completar este campo");
                    $("#multiStepsPass").css("border","solid 1px red");
                    valid = false;

                }

                if ($("#multiStepsConfirmPass").val() == vacio) {

                    $("#multiStepsConfirmPassMsg").text("Debe completar este campo");
                    $("#multiStepsConfirmPass").css("border","solid 1px red");
                    valid = false;

                }
                if ($("#multiStepsConfirmPass").val() != $("#multiStepsPass").val() ) {

                    $("#multiStepsConfirmPassMsg").text("Las contrase\u00f1as no coinciden. Int\xE9ntalo de nuevo.");
                    $("#multiStepsConfirmPass").css("border", "solid 1px red");
                    valid = false;
                }

                validarUsernameExistente();
                validarEmailExistente();


                return valid;

            }

            function validateStep2() {
                resetStep2();
                var valid = true;

                if ($("#multiStepsFirstName").val().trim() == vacio) {

                    $("#multiStepsFirstNameMsg").text("Debe completar este campo");
                    $("#multiStepsFirstName").css("border", "solid 1px red");
                    valid = false;

                }

                if ($("#multiStepsLastName").val() == vacio) {

                    $("#multiStepsLastNameMsg").text("Debe completar este campo");
                    $("#multiStepsLastName").css("border", "solid 1px red");
                    valid = false;

                } 

                if ($("#multiStepsCountry").val() == vacio) {

                    $("#multiStepsCountryMsg").text("Debe completar este campo");
                    $("#multiStepsCountry").css("border", "solid 1px red");
                    valid = false;

                }

                if ($("#multiStepsProvince").val() == vacio) {

                    $("#multiStepsProvinceMsg").text("Debe completar este campo");
                    $("#multiStepsProvince").css("border", "solid 1px red");
                    valid = false;

                }
               

                return valid;

            }

            function validateStep3() {

                resetStep3();
                var valid = true;

                if ($("#multiStepsUniversity").val().trim() == vacio) {

                    $("#multiStepsUniversityMsg").text("Debe completar este campo");
                    $("#multiStepsUniversity").css("border", "solid 1px red");
                    valid = false;

                }

                if ($("#multiStepsLastYear").val() <= 1950 || $("#multiStepsLastYear").val() > new Date().getFullYear()) {

                    $("#multiStepsLastYearMsg").text("Debe completar este campo");
                    $("#multiStepsLastYear").css("border", "solid 1px red");
                    valid = false;

                }

                if ($("#btnTyC").is(':checked') != true) {

                    $("#btnTyCMsg").text("Aceptar condiciones de uso para continuar");
                    valid = false;

                }
                
                return valid;
            }

            function isEmail(email) {
                var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                return regex.test(email);
            }

            let validationStepper = new Stepper(stepsValidation, {
                linear: true
            });

            // Account details
            const multiSteps1 = FormValidation.formValidation(stepsValidationFormStep1, {}).on('core.form.valid', function () {
                // Jump to the next step when all fields in the current step are valid
                if (validateStep1() )
                    validationStepper.next();
            });

            // Personal info
            const multiSteps2 = FormValidation.formValidation(stepsValidationFormStep2, {}).on('core.form.valid', function () {
                // Jump to the next step when all fields in the current step are valid
                if (validateStep2() )
                    validationStepper.next();
            });

            // Social links
            const multiSteps3 = FormValidation.formValidation(stepsValidationFormStep3, {}).on('core.form.valid', function () {
                // You can submit the form
                if (validateStep3() && hecho) {
                    hecho = false;
                    stepsValidationForm.submit()
                    

                }
                // or send the form data to server via an Ajax request
                // To make the demo simple, I just placed an alert
            });

            stepsValidationNext.forEach(item => {
                item.addEventListener('click', event => {
                    // When click the Next button, we will validate the current step
                    switch (validationStepper._currentIndex) {
                        case 0:
                            multiSteps1.validate();
                            break;

                        case 1:
                            multiSteps2.validate();
                            break;

                        case 2:
                            multiSteps3.validate();
                            break;

                        default:
                            break;
                    }
                });
            });

            stepsValidationPrev.forEach(item => {
                item.addEventListener('click', event => {
                    switch (validationStepper._currentIndex) {
                        case 2:
                            validationStepper.previous();
                            break;

                        case 1:
                            validationStepper.previous();
                            break;

                        case 0:

                        default:
                            break;
                    }
                });
            });

            //var url = "https://localhost:7062";
            var url = "https://cresimed.com";

            function validarUsernameExistente() {
                if ($("#multiStepsUsername").val().trim() != "")
                    $.post(url + "/Account/ValidateUsername",
                        { username: $("#multiStepsUsername").val() },
                        function (data, status) {

                            var bool_value = data == "true" ? true : false;

                            if (bool_value) {

                                $("#multiStepsUsernameMsg").text("Ese nombre de usuario ya est\xE1 en uso. Prueba con otro.");
                                $("#multiStepsUsername").css("border", "solid 1px red");

                            } else {
                                $("#multiStepsUsernameMsg").text("");
                                $("#multiStepsUsername").css("border", "solid 0px red");

                            }

                        });

            };

            function validarEmailExistente() {
                if ($("#multiStepsEmail").val().trim() != "")
                    $.post(url + "/Account/ValidateEmail",
                        { email: $("#multiStepsEmail").val() },
                        function (data, status) {

                            var bool_value = data == "true" ? true : false;

                            if (bool_value) {
                                $("#multiStepsEmailMsg").text("Ese email ya est\xE1 en uso. Prueba con otro.");
                                $("#multiStepsEmail").css("border", "solid 1px red");
                            } else {
                                $("#multiStepsEmailMsg").text("");
                                $("#multiStepsEmail").css("border", "solid 0px red");


                            }
                        });

            }

            function validarCodigoDeDescuento() {
                $.post(url + "/Account/ValidateCodigoDeDescuento",
                    { deto: $("#CodigoDeDescuento").val() },
                    function (data, status) {
                        alert();
                    });
            };

            $(document).ready(function () {


                $("#multiStepsUsername").focusout(function () {
                    validarUsernameExistente();
                });

                $("#multiStepsEmail").focusout(function () {
                    validarEmailExistente();
                });

            });
        }
    })();
});
