document.getElementById("table-body").addEventListener("click", async (e) => {
    if (e.target.classList.contains("redirect-link")) {
        e.preventDefault();

        const shortUrl = e.target.textContent;
        const id = e.target.dataset.id;

        const payload = { ShortUrl: shortUrl };

        const res = await fetch("/TableWithUrl/RedirectByShortUrl", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });

        if (res.ok) {
            const data = await res.json();

            document.getElementById(`count-${id}`).textContent = data.countRedirection;
            window.open(data.originalUrl, "_blank");

        } else {
            console.log("Error transition");
        }
    }
});