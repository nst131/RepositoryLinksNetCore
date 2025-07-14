document.addEventListener("DOMContentLoaded", () => {
    const updateModal = document.getElementById("update-modal");
    const closeUpdateBtn = document.getElementById("close-update-modal");
    const updateForm = document.getElementById("update-form");
    const updateInput = document.getElementById("update-url");
    const updateError = document.getElementById("update-error");

    let currentId = null;

    // Открытие модалки через делегирование событий
    document.getElementById("table-body").addEventListener("click", async (e) => {
        if (e.target.classList.contains("update-btn")) {
            e.preventDefault();

            currentId = e.target.dataset.id;

            try {
                const res = await fetch(`/TableWithUrl/Get/${currentId}`);
                if (!res.ok) throw new Error("Error receiving response from update data ");

                const data = await res.json();
                updateInput.value = data.originalUrl;
                updateError.textContent = "";
                updateModal.classList.remove("hidden");
            } catch (err) {
                console.log("Error during update data");
            }
        }
    });

    // Закрытие модалки
    closeUpdateBtn.addEventListener("click", () => {
        updateModal.classList.add("hidden");
        updateInput.value = "";
        updateError.textContent = "";
    });

    // Закрытие при клике вне формы
    updateModal.addEventListener("click", (e) => {
        if (e.target === updateModal) {
            updateModal.classList.add("hidden");
            updateInput.value = "";
            updateError.textContent = "";
        }
    });

    // Обработка формы
    updateForm.addEventListener("submit", async (e) => {
        e.preventDefault();
        updateError.textContent = "";

        const payload = {
            Id: currentId,
            OriginalUrl: updateInput.value
        };

        const res = await fetch("/TableWithUrl/Update", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });

        if (res.ok) {
            // Обновить строку в таблице
            const row = document.getElementById(currentId);
            const urlCell = row.querySelector("td");
            urlCell.textContent = updateInput.value;

            updateModal.classList.add("hidden");
            updateInput.value = "";
            currentId = null;
        } else {
            const data = await res.json();
            updateError.innerHTML = data.errors?.join("<br>") || "Error Update";
        }
    });
});