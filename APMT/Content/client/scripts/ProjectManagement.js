
function setStatus(id) {
    $.ajax({
        url: "/Company/ProjectManagement/setStatus/" + id,
        success: function (response) {
            console.log(response);
        }
    });
}


  