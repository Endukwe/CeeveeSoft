

const copyButton = document.getElementById('btncopy');
copyButton.addEventListener('click', async (event) => {
    const content = document.getElementById('content').textContent;
    await navigator.clipboard.writeText(content);
    console.log(content);
    const copied = await navigator.clipboard.readText();
    console.log(copied);
})