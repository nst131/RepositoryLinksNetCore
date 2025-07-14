document.addEventListener("DOMContentLoaded", () => {
    const modal = document.getElementById("create-modal");
    const closeBtn = document.getElementById("close-modal");
    const form = document.getElementById("create-form");
    const input = form.querySelector("input[name='OriginalUrl']");
    const errorDiv = document.getElementById("url-error");
    const openBtn = document.getElementById("create-link-btn");

    // Открытие модалки
    openBtn.addEventListener("click", () => {
        input.value = "";
        errorDiv.textContent = "";
        modal.classList.remove("hidden");
    });

    // Закрытие модалки
    closeBtn.addEventListener("click", () => {
        modal.classList.add("hidden");
    });

    // Закрытие при клике вне формы
    modal.addEventListener("click", (e) => {
        if (e.target === modal) {
            modal.classList.add("hidden");
        }
    });

    // Обработка формы
    form.addEventListener("submit", async (e) => {
        e.preventDefault();
        errorDiv.textContent = "";

        const payload = { OriginalUrl: input.value };
        let res = null;
        let data = null;

        try {
            res = await fetch("/TableWithUrl/Create", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(payload)
            });

            data = await res.json();
        }
        catch {
            console.log("Error during creation url");
        }

        if (res.ok) {
            // Обновить строку в таблице
            modal.classList.add("hidden");

            const tbody = document.getElementById("table-body");
            const row = document.createElement("tr");
            row.setAttribute("id", data.id);

            row.innerHTML = `
                <td>${data.originalUrl}</td>
                <td>
                  <a class="redirect-link" data-id="${data.id}">${data.shortUrl.trim()}</a>
                </td>
                <td>${data.createdAt}</td>
                <td id="count-${data.id}">${data.countRedirection}</td>
                <td>
                  <button class="btn btn-sm btn-danger delete-btn" data-id="${data.id}">delete</button>
                  <button class="btn btn-sm btn-warning update-btn" data-id="${data.id}">update</button>
                </td>
            `;

            tbody.appendChild(row);
        } else {
            errorDiv.innerHTML = data.errors?.join("<br>") || "Error creation";
        }
    });
});