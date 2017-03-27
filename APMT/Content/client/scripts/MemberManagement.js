$(document).ready(function () {
    getMember();
});
function getMember() {
    $.ajax({
        url: "/Company/MemberManagement/getMember/",
        type: 'GET',
        datatype: 'json',
        success: function (response) {
            var row = "";
            $.each(response.lisMember, function (i, item) {
                row += "<tr>"
                row += "<td>" + (i + 1) + "</td>"
                row += "<td>" + item.fullName + "</td>"
                // IsAdministrator
                if (+item.isAdministrator == true) {
                    row += "<td>" + "<input class='icheckAdmin' type='checkbox' checked='checked' onclick ='return setAdmin(" + item.id + ")'/> Administrator </br>"
                }
                else {
                    row += "<td>" + "<input class='icheckAdmin' type='checkbox' onclick ='return setAdmin(" + item.id + ")'/> Administrator </br>"
                }
                // IsCreator
                if (+item.isCreator == true) {
                    row += "<input class='icheckCreator' type='checkbox' checked='checked' onclick ='return setCreator(" + item.id + ")'/>Creator" + "</td>"
                }
                else {
                    row += "<input class='icheckCreator' type='checkbox' onclick ='return setCreator(" + item.id + ")'/>Creator" + "</td>"
                }

                row += "<td>"
                row += "<center>"
                //Status
                if (+item.isAllowed == 1) {
                    row += "<center><input class='icheckStatus' type='checkbox' checked='checked' onclick ='return setStatus(" + item.id + ")'/> </center></td>"
                }
                else {
                    row += "<center><input class='icheckStatus' type='checkbox' onclick ='return setStatus(" + item.id + ")' /> </center></td>"
                }
                //Btn Delete
                row += "<td><center>"
                   + " <button class='btn btn-danger btnDeleteMember' type='button' aria-haspopup='true' aria-expanded='true' onclick ='return deleteMember(" + item.id + ")'>"
                    + "Delete</button></center></td>"
                row += "</tr>";
                $("#lstMember tbody").html(row);
            });
        },
        error: function (err) {
          //  alert("Error: " + err.responseText);
        }
    });
     
}

function setAdmin(id) {
    $.ajax({
        url: "/Company/MemberManagement/setAdministrator/" + id,
        datatype: "json",
        success: function (response) {
            console.log(response.isAdministrator);
        }
    });
}
function setCreator(id) {
    $.ajax({
        url: "/Company/MemberManagement/setCreator/" + id,
        data: { id: id },
        datatype: "json",
        success: function (response) {
        }
    });
}
function deleteMember(id) {
    $("#confirm-delete").modal('show');
    $("#confirm-delete #btnOK").click(function (e) {
        $.ajax({
            url: "/Company/MemberManagement/deleteMember/" + id,
            success: function (response) {
                getMember();
                $("#confirm-delete").modal('hide');
            },
            error: function (err) {
                $("#confirm-delete").modal('hide');
                alert("Error: Can't Delete this member!");
            }
        });
    });

}
function setStatus(id) {
    $.ajax({
        url: "/Company/MemberManagement/setStatus/" + id,
        success: function (response) {
            console.log(response);
        }
    });
}

// Add_New View
$('#btnAdd').click(function (e) {
    var stringEmail = $("#somevalue").val();
    $.ajax({
        url: "/Company/MemberManagement/Add_New",
        data: { stringEmail: stringEmail },
        datatype: "json",
        success: function (respone) {
            $("#txtResult").text(respone.mesg);
            clear();
            getMember();
        }
    });
  
});
$('#btnClose').click(function (e) {
    clear();
    $("#Add_New_Modal").modal('hide');
});
function clear() {
    $("#somevalue").val("");
    $("#txtResult").text("");
}
$(document).ready(function (request) {
    $("#somevalue").autocomplete({
        source: 'autocomplete',
        data: "{term :" + request.term + "}",
    }   
    )
})