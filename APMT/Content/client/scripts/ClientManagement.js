function setStatus(id) {
    $.ajax({
        url: "/System/ClientManagement/setStatus/" + id,
        success: function (response) {
            console.log(response);
        }
    });
}