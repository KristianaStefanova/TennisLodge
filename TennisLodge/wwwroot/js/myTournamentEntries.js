//// Archivo: wwwroot/js/myTournamentEntries.js

//document.addEventListener("DOMContentLoaded", async () => {
//    const container = document.getElementById("my-tournaments-container");

//    try {
//        const response = await fetch("https://localhost:7286/api/TournamentEntryApi/MyEntries", {
//            method: "GET",
//            credentials: "include"
//        });

//        if (response.status === 401) {
//            container.innerHTML = `<div class="alert alert-warning text-center">You must be logged in to view your tournaments.</div>`;
//            return;
//        }

//        if (!response.ok) {
//            container.innerHTML = `<div class="alert alert-danger text-center">Failed to load your tournament entries.</div>`;
//            return;
//        }

//        const entries = await response.json();

//        if (!entries || entries.length === 0) {
//            container.innerHTML = `<div class="alert alert-info text-center">You are not registered in any tournaments yet.</div>`;
//            return;
//        }

//        let tableHtml = `
//            <table class="table table-striped table-hover align-middle">
//                <thead class="table-primary">
//                    <tr>
//                        <th>Tournament Name</th>
//                        <th>Start Date</th>
//                        <th>Location</th>
//                        <th>Registered On</th>
//                        <th class="text-end">Actions</th>
//                    </tr>
//                </thead>
//                <tbody>
//        `;

//        entries.forEach(entry => {
//            tableHtml += `
//                <tr>
//                    <td class="fw-semibold">${entry.tournamentName}</td>
//                    <td>${new Date(entry.tournamentStartDate).toLocaleDateString(undefined, { month: 'short', day: 'numeric', year: 'numeric' })}</td>
//                    <td>${entry.tournamentLocation}</td>
//                    <td>${new Date(entry.registeredOn).toLocaleString(undefined, { month: 'short', day: 'numeric', year: 'numeric', hour: '2-digit', minute: '2-digit' })}</td>
//                    <td class="text-end">
//                        <form action="/TournamentEntry/CancelEntry" method="post" class="d-inline" onsubmit="return confirm('Are you sure you want to cancel this entry?');">
//                            <input type="hidden" name="entryId" value="${entry.entryId}" />
//                            <button type="submit" class="btn btn-sm btn-outline-danger">Cancel</button>
//                        </form>
//                    </td>
//                </tr>
//            `;
//        });

//        tableHtml += `</tbody></table>`;
//        container.innerHTML = tableHtml;

//    } catch (error) {
//        container.innerHTML = `<div class="alert alert-danger text-center">An error occurred while loading your tournaments.</div>`;
//        console.error("Error loading tournament entries:", error);
//    }
//});

// Archivo: wwwroot/js/myTournamentEntries.js

//$(function () {
//    const $container = $("#my-tournaments-container");

//    $.ajax({
//        url: "http://localhost:7286/api/TournamentEntryApi/MyEntries",
//        method: "GET",
//        contentType: "application/json",
//        xhrFields: {
//            withCredentials: true 
//        },
//        crossDomain: true,
//        success: function (entries) {
//            if (!entries || entries.length === 0) {
//                $container.html(`<div class="alert alert-info text-center">You are not registered in any tournaments yet.</div>`);
//                return;
//            }

//            let tableHtml = `
//                <table class="table table-striped table-hover align-middle">
//                    <thead class="table-primary">
//                        <tr>
//                            <th>Tournament Name</th>
//                            <th>Start Date</th>
//                            <th>Location</th>
//                            <th>Registered On</th>
//                            <th class="text-end">Actions</th>
//                        </tr>
//                    </thead>
//                    <tbody>
//            `;

//            entries.forEach(entry => {
//                const startDate = new Date(entry.tournamentStartDate).toLocaleDateString(undefined, { month: 'short', day: 'numeric', year: 'numeric' });
//                const registeredOn = new Date(entry.registeredOn).toLocaleString(undefined, { month: 'short', day: 'numeric', year: 'numeric', hour: '2-digit', minute: '2-digit' });

//                tableHtml += `
//                    <tr>
//                        <td class="fw-semibold">${entry.tournamentName}</td>
//                        <td>${startDate}</td>
//                        <td>${entry.tournamentLocation}</td>
//                        <td>${registeredOn}</td>
//                        <td class="text-end">
//                            <form action="/TournamentEntry/CancelEntry" method="post" class="d-inline cancel-entry-form">
//                                <input type="hidden" name="entryId" value="${entry.entryId}" />
//                                <button type="submit" class="btn btn-sm btn-outline-danger">Cancel</button>
//                            </form>
//                        </td>
//                    </tr>
//                `;
//            });

//            tableHtml += `</tbody></table>`;
//            $container.html(tableHtml);
//        },
//        statusCode: {
//            401: function () {
//                $container.html(`<div class="alert alert-warning text-center">You must be logged in to view your tournaments.</div>`);
//            }
//        },
//        error: function () {
//            $container.html(`<div class="alert alert-danger text-center">Failed to load your tournament entries.</div>`);
//        }
//    });

//    $container.on("submit", ".cancel-entry-form", function (e) {
//        e.preventDefault();
//        const form = this;

//        Swal.fire({
//            title: 'Are you sure you want to cancel this entry?',
//            icon: 'warning',
//            showCancelButton: true,
//            confirmButtonText: 'Yes, cancel it',
//            cancelButtonText: 'No, keep it'
//        }).then((result) => {
//            if (result.isConfirmed) {
//                form.submit();
//            }
//        });
//    });
//});

