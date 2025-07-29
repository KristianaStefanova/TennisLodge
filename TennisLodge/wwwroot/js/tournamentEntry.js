document.addEventListener("DOMContentLoaded", function () {
    $(".join-tournament-btn").on("click", async function () {
        const button = $(this);
        const tournamentId = button.data("tournament-id");

        if (!tournamentId) {
            console.warn("❌ Tournament ID not found on the button.");
            return;
        }

        try {
            const params = new URLSearchParams({ tournamentId: tournamentId });

            const response = await fetch(`/api/TournamentEntryApi/Join?${params.toString()}`, {
                method: "POST",
                credentials: "include"
            });

            if (response.ok) {
                // ✅ Cambiar botón
                button.removeClass("btn-primary")
                    .addClass("btn-outline-secondary")
                    .html('<i class="bi bi-check2-circle me-2"></i> Already Joined')
                    .prop("disabled", true);

                // ✅ Incrementar contador
                const countSpan = $(`.joined-count[data-tournament-id="${tournamentId}"]`);
                if (countSpan.length) {
                    let currentCount = parseInt(countSpan.text(), 10);
                    if (!isNaN(currentCount)) {
                        countSpan.text(currentCount + 1);
                    }
                }

                // ✅ Mensaje
                Swal.fire({
                    icon: "success",
                    title: "You have successfully joined the tournament!",
                    showConfirmButton: false,
                    timer: 2000
                });
            } else {
                console.info("Join request rejected (maybe already joined), no action taken.");
            }
        } catch (error) {
            console.error("❌ Error joining tournament:", error);
        }
    });
});
