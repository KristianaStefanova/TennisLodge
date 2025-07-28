document.addEventListener('DOMContentLoaded', () => {
    const buttons = document.querySelectorAll('.join-btn');

    // Función para obtener el token antiforgery
    const getRequestVerificationToken = () => {
        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        return tokenInput ? tokenInput.value : '';
    };

    // Función para mostrar toast de éxito o error
    const showToast = (message, isSuccess = true) => {
        const toastId = 'toast-' + Date.now();
        const toastHtml = `
            <div id="${toastId}" class="toast align-items-center text-white ${isSuccess ? 'bg-success' : 'bg-danger'} border-0 mb-2" role="alert" aria-live="assertive" aria-atomic="true">
                <div class="d-flex">
                    <div class="toast-body">
                        ${message}
                    </div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
            </div>`;

        const container = document.getElementById('toast-container');
        if (container) {
            container.insertAdjacentHTML('beforeend', toastHtml);
            const toastElement = document.getElementById(toastId);
            const bsToast = new bootstrap.Toast(toastElement, { delay: 4000 });
            bsToast.show();

            // Auto remover después de ocultarse
            toastElement.addEventListener('hidden.bs.toast', () => toastElement.remove());
        }
    };

    buttons.forEach(button => {
        button.addEventListener('click', async function (e) {
            e.preventDefault();

            if (this.disabled) return;

            this.disabled = true;

            const tournamentId = this.dataset.tournamentId;

            try {
                const response = await fetch('/api/TournamentEntryApi/Join', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': getRequestVerificationToken(),
                    },
                    body: JSON.stringify({ tournamentId })
                });

                if (response.ok) {
                    // Cambiar botón a "Already Joined"
                    this.outerHTML = `
                        <button class="btn btn-outline-secondary w-100" disabled>
                            <i class="bi bi-check2-circle me-2"></i> Already Joined
                        </button>`;

                    // Actualizar contador si existe
                    const countSpan = document.querySelector(`.joined-count[data-tournament-id="${tournamentId}"]`);
                    if (countSpan) {
                        let currentCount = parseInt(countSpan.textContent);
                        if (!isNaN(currentCount)) {
                            countSpan.textContent = currentCount + 1;
                        }
                    }

                    // Mostrar toast de éxito
                    showToast('You have successfully joined the tournament!', true);
                } else {
                    let errorText = await response.text();
                    try {
                        const errorJson = JSON.parse(errorText);
                        errorText = errorJson.message || errorText;
                    } catch {
                        // no es JSON
                    }
                    showToast("Could not join tournament: " + errorText, false);
                    this.disabled = false;
                }
            } catch (ex) {
                showToast("Network error or server unreachable.", false);
                this.disabled = false;
            }
        });
    });
});
