$(document).ready(function () {
    $(function () {
        //popover funtion
        $("[data-toggle=popover]").popover({
            html: true,
            content: function () {
                var content = $(this).attr("data-popover-content");
                return $(content).children(".popover-body").html();
            },
            title: function () {
                var title = $(this).attr("data-popover-content");
                return $(title).children(".popover-heading").html();
            }
        });
    });
    //tooltip function
    $('[data-toggle="tooltip"]').tooltip();
});

    //Common search function
    $(".SearchTxtbox").keyup(function () {

    //split the current value of searchInput
    var data = this.value.split(" ");
            //create a jquery object of the rows
            if ($('#dLabel').text() != "ALL") {
                var jo = $(".BlueTable > tbody").find(".next");
            }
            else {
                var jo = $(".BlueTable > tbody").find("tr");
            }


    if (this.value == "") {
        jo.show();
    return;
    }
    //hide all the rows
    jo.hide();

    //Recusively filter the jquery object to get results.
            jo.filter(function (i, v) {
                var $t = $(this);
                for (var d = 0; d < data.length; ++d) {
                    if ($t.is(":contains('" + data[d] + "')")) {
                return true;
            }
        }
        return false;
    })
    //show the rows that match.
    .show();
}).focus(function () {
        this.value = "";
    $(this).css({
        "color": "black"
    });
    $(this).unbind('focus');
}).css({
        "color": "#C0C0C0"
            });


//common success alert box display function
function displaySuccess(msg) {
   
    $('#SuccessAlert').css('display', 'block');
    $('#SuccessMsg').text(msg);
};

//common failure alert box display function
function displayFailure() {
    
    $('#FailureAlert').css('display', 'block');
    
};

//common visibility function used to hide and show text in specified textbox can be found in login.aspx Password textbox
$('#visibility_btn').mousedown(function () {
    $('.fa-eye').css("display", "block");
    $('.fa-eye-slash').css("display", "none");
    $('#Password_Login').attr("type", "text");
});
$('#visibility_btn').mouseup(function () {
    $('.fa-eye').css("display", "none");
    $('.fa-eye-slash').css("display", "block");
    $('#Password_Login').attr("type", "password");
});

//common dropdown function used for displaying child elements in anchor tag, can be found on all management pages
$(".customDropdown").hover(
    function () {
        $('>.dropdown-menu', this).stop(true, true).fadeIn("fast");
        $(this).addClass('open');
    },
    function () {
        $('>.dropdown-menu', this).stop(true, true).fadeOut("fast");
        $(this).removeClass('open');
    });

$('.dropdown-item').click(function () {
    var selected = $(this).text();
    $('.customDropdown').html(selected);
    $('#ContentPlaceHolder1_hiddenvalue').val(selected);
});


//common function to validate email of any textbox
function validateEmail(sEmail) {
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (filter.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
};

























