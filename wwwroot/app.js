function switchTab(tabId) {
    // Update buttons
    document.querySelectorAll('.tab-btn').forEach(btn => btn.classList.remove('active'));
    document.getElementById(`tab-${tabId}`).classList.add('active');

    // Update content visibility
    document.querySelectorAll('.tab-content').forEach(content => content.classList.remove('active'));
    document.getElementById(`${tabId}-section`).classList.add('active');
}

async function solveNumbers() {
    const numbersInput = document.getElementById('numbers-input').value.trim();
    const targetInput = document.getElementById('target-input').value.trim();
    const resultsContainer = document.getElementById('numbers-results');

    if (!numbersInput || !targetInput) {
        showError(resultsContainer, "Please enter both numbers and a target.");
        return;
    }

    setLoading(resultsContainer, document.getElementById('btn-numbers'));

    try {
        const response = await fetch(`/api/numbers?numbers=${encodeURIComponent(numbersInput)}&target=${encodeURIComponent(targetInput)}`);
        
        if (!response.ok) throw new Error("Server error");
        
        const data = await response.json();
        
        if (data.error) throw new Error(data.error);
        if (!data || data.length === 0) {
            resultsContainer.innerHTML = `<div class="error-msg">No mathematically exact solutions found! 🥺</div>`;
            return;
        }

        renderNumbersResults(resultsContainer, data);
    } catch (err) {
        showError(resultsContainer, "Failed to calculate: " + err.message);
    } finally {
        resetButton(document.getElementById('btn-numbers'), "Solve Numbers");
    }
}

async function solveLetters() {
    const lettersInput = document.getElementById('letters-input').value.trim();
    const resultsContainer = document.getElementById('letters-results');

    if (!lettersInput) {
        showError(resultsContainer, "Please enter some letters.");
        return;
    }

    setLoading(resultsContainer, document.getElementById('btn-letters'));

    try {
        const response = await fetch(`/api/letters?q=${encodeURIComponent(lettersInput)}`);
        
        if (!response.ok) throw new Error("Server error");
        
        const data = await response.json();
        
        if (!data || data.length === 0) {
            resultsContainer.innerHTML = `<div class="error-msg">No valid English words found! 🥺</div>`;
            return;
        }

        renderLettersResults(resultsContainer, data);
    } catch (err) {
        showError(resultsContainer, "Failed to search words: " + err.message);
    } finally {
        resetButton(document.getElementById('btn-letters'), "Find Words");
    }
}

function renderNumbersResults(container, solutions) {
    container.innerHTML = '';
    
    // Add staggered animation delay
    solutions.forEach((sol, index) => {
        const div = document.createElement('div');
        div.className = 'result-item';
        div.style.animationDelay = `${index * 0.05}s`;
        
        div.innerHTML = `
            <div class="result-equation">${sol.expression}</div>
            <div class="result-target">= ${sol.result}</div>
        `;
        container.appendChild(div);
    });
}

function renderLettersResults(container, words) {
    container.innerHTML = '';
    
    // Display top 50 to prevent DOM lag on generic letters
    const displayWords = words.slice(0, 50);
    
    displayWords.forEach((wordObj, index) => {
        const div = document.createElement('div');
        div.className = 'result-item';
        div.style.animationDelay = `${index * 0.02}s`;
        
        div.innerHTML = `
            <div class="result-word">${wordObj.word}</div>
            <div class="result-length">${wordObj.length} LTR</div>
        `;
        container.appendChild(div);
    });

    if (words.length > 50) {
        const div = document.createElement('div');
        div.className = 'loading';
        div.innerText = `+ ${words.length - 50} more results hidden to save your browser!`;
        container.appendChild(div);
    }
}

function setLoading(container, button) {
    container.innerHTML = '<div class="loading">Calculating the impossible... ✨</div>';
    button.disabled = true;
    button.innerText = 'Calculating...';
}

function resetButton(button, text) {
    button.disabled = false;
    button.innerText = text;
}

function showError(container, message) {
    container.innerHTML = `<div class="error-msg">${message}</div>`;
}

// Map 'Enter' key to triggering the search
document.getElementById('target-input').addEventListener('keypress', function (e) {
    if (e.key === 'Enter') solveNumbers();
});
document.getElementById('letters-input').addEventListener('keypress', function (e) {
    if (e.key === 'Enter') solveLetters();
});
