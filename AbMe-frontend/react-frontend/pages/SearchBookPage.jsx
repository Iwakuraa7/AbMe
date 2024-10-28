import { useEffect, useState } from "react"

const API_KEY = 'AIzaSyCBgSTwhLqLHJjXx9dHtT4Qk5PnYOVCBos';

export default function SearchBookPage() {
    const [searchInput, setSearchInput] = useState('');
    const [booksData, setBooksData] = useState([]);
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
        console.log("adding book to ur profile...");
    }

    return (
        <>
        <input type='text' onChange={(e) => {setSearchInput(e.target.value)}}></input>
        <button onClick={handleSearch}>Search</button><br/>
        {booksData && <h2>Books</h2>}
        <div className='searchBox'>
            {booksData && booksData.map((book, index) => {
                return(
                    <div
                    key={index}
                    className="searchResultEntity"
                    onMouseEnter={() => {setCurrentBookData(book)}}
                    onMouseLeave={() => {setCurrentBookData(null)}}
                    >
                        <img src={book.volumeInfo.imageLinks ? book.volumeInfo.imageLinks.thumbnail : null}></img><br/>
                        <h3>{book.volumeInfo.title}</h3>
                        {currentBookData === book && (
                            <button onClick={() => addBookData(book)}>Add</button>
                        )}
                    </div>
                )
            })}
        </div>
        </>
    )
}