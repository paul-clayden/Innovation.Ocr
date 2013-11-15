//$.ready(function () {
    //$("p").click(function () {
    //    $(this).hide();
    //});

    function PostImage() {
        $.ajax({
            type: "POST",
            url: "api/reg",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function (xhr) {
                //xhr.setRequestHeader('X-FileName',file.name);
                alert('test');
            },
            success: function (result) {
                alert(result.d);
            },
            fail: function (response, status) {
                alert("error : " + status);
            }
        });
    }

        //$('input[type="file"]').ajaxfileupload({
        //    action: '/api/reg',
        //    onComplete: function (response) {
        //        alert(JSON.stringify(response));
        //    },
        //    error: function (data, status, e) {
        //        alert(e);
        //    }
        //});
    
//});