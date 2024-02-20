window.doSome = (array) => {
    const squares = document.querySelectorAll('.bb-container > *')

    console.log(squares)
    console.log(array.board[0][0])

    const gameBoard = array.board

    for (let i = 0; i < 4; i++) {
        for (let j = 0; j < 4; j++) {
            squares[j + i * 4].textContent = gameBoard[i][j] !== 0 ? gameBoard[i][j] : ''
        }
    }

};