<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IPS_Prototype.Login_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Script/jquery-3.3.1.min.js"></script>
    <script src="Script/bootstrap.bundle.min.js"></script>
     <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/IPS_vertical.css" rel="stylesheet" />
    <link href="css/fontawesome-all.css" rel="stylesheet" />
    <script src="Script/Chart.bundle.js"></script>
    <script>
        $( document ).ready(function() {
             $('.Login_Wtxt.user').focus(function () {
             $(this).addClass('active');
             $('.Login_lbl.user').addClass('active');
             });

             $('.Login_txt.pass').focus(function () {
             $(this).addClass('active');
             $('.Login_lbl.pass').addClass('active');
             });

            $('.Login_txt').focusout(function () {
                if ($('.Login_txt.user').val() == '') {
                    $('.Login_txt.user').removeClass('active');
                    $('.Login_lbl.user').removeClass('active');
                }
                else {
                    $('.Login_txt.user').addClass('active');
                    $('.Login_lbl.user').addClass('active');
                }
    
                if ($('.Login_txt.pass').val() == '') {
                    $('.Login_txt.pass').removeClass('active');
                    $('.Login_lbl.pass').removeClass('active');
                }
                else {
                    $('.Login_txt.pass').addClass('active');
                    $('.Login_lbl.pass').addClass('active');
                }
            });

        });
    </script>
    <title></title>
</head>
<body id="LoginBody">
    <form id="form1" runat="server">
        <div class="content-area" style="padding-left:0px; padding-right:0px; padding-top:0px;">
            <canvas id="canvas" style="height:24em; width:100%; background:none; "></canvas>
           
            <div id="LoginQuote_Wrapper" style="cursor:default;">
                <i>
                "ENGAGING MINDS, EXCHANGING IDEAS" <br /><br /> - INSTITUTE OF POLICY STUDIES
                    </i>
            </div>

            <div id="LoginWrapper">
                <h1 id="LoginHeader"><img src="../Images/IPS_Logo_long.PNG"/></h1>
                <div id="LoginContent">
                    <div id="Login_Username">

                <label style="display:block;" class="Login_lbl user">User ID</label>
                <input type="text" runat="server" id="User_Login" class="Login_txt user" style="display:block;" />
                        </div>
         <div id="Login_Password">
                <label style="display:block;" class="Login_lbl pass">Password</label>
                        
                <input type="password" runat="server" id="Password_Login" class="Login_txt pass" style="display:block;" />
                <button type="button" id="visibility_btn" class="inner_btn"><i class="fas fa-eye-slash" style="display:block;"></i><i class="fas fa-eye" style="display:none;"></i></button>
                </div>
                    </div>
                    <label runat="server" id="Login_Alert" class="Alert_Message" style="display:none;">*Password/User ID INCORRECT</label>
                    <div id="LoginBtn_Wrapper">
                    
                    <button runat="server" type="button" id="Login_btn" class="btn btn-primary LoginBtn" style="position:relative;" onserverclick="Login_Click">Login</button>
                    
                        </div>
                    
            </div>
               
            </div>
                
            
            
            
     
        <script>
            var canvas = document.getElementById("canvas"),
    ctx = canvas.getContext('2d');

canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

var stars = [], // Array that contains the stars
    FPS = 60, // Frames per second
    x = 70, // Number of stars
    mouse = {
        x: 0,
        y: 0
    };  // mouse location

// Push stars to array

for (var i = 0; i < x; i++) {
    stars.push({
        x: Math.random() * canvas.width,
        y: Math.random() * canvas.height,
        radius: Math.random() * 1 + 1,
        vx: Math.floor(Math.random() * 50) - 25,
        vy: Math.floor(Math.random() * 50) - 25
    });
}

// Draw the scene

function draw() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    ctx.globalCompositeOperation = "lighter";

    for (var i = 0, x = stars.length; i < x; i++) {
        var s = stars[i];

        ctx.fillStyle = "#fff";
        ctx.beginPath();
        ctx.arc(s.x, s.y, s.radius, 0, 2 * Math.PI);
        ctx.fill();
        ctx.fillStyle = 'black';
        ctx.stroke();
    }

    ctx.beginPath();
    for (var i = 0, x = stars.length; i < x; i++) {
        var starI = stars[i];
        ctx.moveTo(starI.x, starI.y);
        if (distance(mouse, starI) < 150) ctx.lineTo(mouse.x, mouse.y);
        for (var j = 0, x = stars.length; j < x; j++) {
            var starII = stars[j];
            if (distance(starI, starII) < 150) {
                //ctx.globalAlpha = (1 / 150 * distance(starI, starII).toFixed(1));
                ctx.lineTo(starII.x, starII.y);
            }
        }
    }
    ctx.lineWidth = 0.05;
    ctx.strokeStyle = 'white';
    ctx.stroke();
}

function distance(point1, point2) {
    var xs = 0;
    var ys = 0;

    xs = point2.x - point1.x;
    xs = xs * xs;

    ys = point2.y - point1.y;
    ys = ys * ys;

    return Math.sqrt(xs + ys);
}

// Update star locations

function update() {
    for (var i = 0, x = stars.length; i < x; i++) {
        var s = stars[i];

        s.x += s.vx / FPS;
        s.y += s.vy / FPS;

        if (s.x < 0 || s.x > canvas.width) s.vx = -s.vx;
        if (s.y < 0 || s.y > canvas.height) s.vy = -s.vy;
    }
}

canvas.addEventListener('mousemove', function (e) {
    mouse.x = e.clientX;
    mouse.y = e.clientY;
});

// Update and draw

function tick() {
    draw();
    update();
    requestAnimationFrame(tick);
}

tick();

            
        </script>
        <script src="Script/IPS_Vertical.js"></script>
        
    </form>
    
</body>
</html>
