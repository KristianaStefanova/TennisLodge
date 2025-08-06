function setRoleValue(userId, inputId) {
    var dropdown = document.getElementById('roleDropdown-' + userId);
    var input = document.getElementById(inputId + '-' + userId);
    input.value = dropdown.value;
}

function confirmRemoveRole(userId, role) {
    var url = `/Admin/UserManagement/ConfirmRemoveRole?userId=${userId}&role=${encodeURIComponent(role)}`;
    window.location.href = url;
    return false;
}

function confirmDeleteUser(userId, userEmail) {
    var url = `/Admin/UserManagement/ConfirmDeleteUser?userId=${userId}`;
    window.location.href = url;
    return false;
}

function confirmAssignRole(userId, role) {
    var url = `/Admin/UserManagement/ConfirmAssignRole?userId=${userId}&role=${encodeURIComponent(role)}`;
    window.location.href = url;
    return false;
}