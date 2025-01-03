import { useContext, useState } from "react"
import styles from "../styles/SearchPage.module.css"
import NavBar from "../components/NavBar";
import { UserContext } from "../src/contexts/UserContext.jsx";
import ContextMessage from "../components/ContextMessage.jsx";

export default function SearchBookPage() {
    const {API_KEY, contextMsg, setContextMsg, fadeOut, setFadeOut, showMessage} = useContext(UserContext);
    const notFoundImage = 'https://i.imgur.com/soXyjFr.jpeg';
    const [searchInput, setSearchInput] = useState('');
    const [booksData, setBooksData] = useState(null);
    const [currentBookData, setCurrentBookData] = useState([]);

    async function handleSearch() {
        var response = await fetch(`https://www.googleapis.com/books/v1/volumes?q=${searchInput}&key=${API_KEY}`);
        var data = await response.json();

        if(data.items) {
            setBooksData(data.items);
            console.log(data.items);
        }
        else {
            console.error("sth went wrong during GET request...");
        }
    }

    async function addBookData(book) {
        try
        {
            var response = await fetch("http://localhost:5078/api/book/create", {
                method: "POST",
                headers: {
                    "Authorization": "Bearer " + localStorage.getItem('token'),
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    Title: book.volumeInfo.title,
                    Author: book.volumeInfo.authors[0],
                    ImageUrl: book.volumeInfo.imageLinks.thumbnail
                })
            })
        
            var data = await response.json();
        
            showMessage(data.message);
        }
        catch(err)
        {
            console.error(err);
        }
    }

    return (
        <>
        <NavBar/>
        <div className={styles["search-main-box"]}>
            {contextMsg && (<ContextMessage message={contextMsg} fadeOut={fadeOut}/>)}

            <div className={styles["search-input-box"]}>
                <h1>Add book to profile</h1>
                <div className={styles["search-and-button-rel"]}>
                    <input
                    type='text'
                    onChange={(e) => {setSearchInput(e.target.value)}}
                    onKeyDown={(e) => {
                        if(e.key === 'Enter')
                            handleSearch();
                    }}
                    />
                    <button onClick={handleSearch}>Search</button><br/>
                </div>
            </div>

            <div className={styles["center-text-box"]}>
                {booksData && <h2>Books</h2>}
            </div>

            <div className={styles["searchBox"]}>
                {booksData && booksData.map((book, index) => {
                    return(
                        <div
                        key={index}
                        className={styles["searchResultEntity"]}
                        onMouseEnter={() => {setCurrentBookData(book)}}
                        onMouseLeave={() => {setCurrentBookData(null)}}
                        >
                            <img src={book.volumeInfo.imageLinks ? book.volumeInfo.imageLinks.thumbnail : notFoundImage}></img><br/>
                            <h3>{book.volumeInfo.title}</h3>
                            {currentBookData === book && (
                                <button onClick={() => addBookData(currentBookData)}>Add</button>
                            )}
                        </div>
                    )
                })}
            </div>
        </div>
        </>
    )
}