document.addEventListener("DOMContentLoaded", () => {
    const tableBody = document.getElementById("table-body");

    tableBody.addEventListener("click", async (e) => {
        if (e.target.classList.contains("delete-btn")) {
            e.preventDefault();

            const id = e.target.dataset.id;

            try {
                const response = await fetch(`/TableWithUrl/Delete/${id}`, {
                    method: "GET"
                });

                if (response.ok) {
                    e.target.closest("tr").remove();
                } else {
                    console.log("Error during removal");
                }
            } catch (err) {
                console.log("Incorrect query:", err);
            }
        }
    });
});