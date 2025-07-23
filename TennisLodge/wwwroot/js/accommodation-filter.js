document.addEventListener("DOMContentLoaded", function () {
    let cityFilter = document.getElementById("cityFilter");
    let tableRows = document.querySelectorAll("table#accommodationTable tbody tr");

    function filterAccommodations() {
        let selectedCity = cityFilter.value.toLowerCase();

        tableRows.forEach(row => {
            let city = row.getAttribute("data-city").toLowerCase();
            let matchesCity = selectedCity === "" || city === selectedCity;

            row.style.display = matchesCity ? "" : "none";
        });
    }

    cityFilter.addEventListener("change", filterAccommodations);
});
