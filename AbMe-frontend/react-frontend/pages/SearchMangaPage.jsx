import { useState } from "react";
import styles from "../styles/SearchPage.module.css";
import NavBar from "../components/NavBar";

export default function SearchMangaPage() {
    const [searchInput, setSearchInput] = useState(null);
    const [currentMangaData, setCurrentMangaData] = useState(null);
    const [mangaData, setMangaData] = useState(null);

    async function handleSearch() {
        var response = await fetch(`https://api.jikan.moe/v4/manga?q=${searchInput}`);
        var data = await response.json();

        var filteredData = data.data.map(anime => ({
            title: [anime.title_english, anime.title],
            imageUrl: anime.images.jpg
        }))

        setMangaData(filteredData);

        console.log(filteredData);
        console.log(data.data);
    }

    async function addMangaData () {
        var response = await fetch("http://localhost:5078/api/manga/create", {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Title: currentMangaData.title[0],
                ImageUrl: currentMangaData.imageUrl.image_url
            })
        });

        var data = await response.json();

        if(data.succeeded)
            console.log("Successfully created manga entity");
        else
            console.log(data.message);        
    }

    return (
        <>
        <NavBar/>
        <div className={styles["search-main-box"]}>
            <div className={styles["search-input-box"]}>
                <h1>Add manga to profile</h1>
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

            <div className={styles["searchBox"]}>
                {mangaData && mangaData.map((manga, index) => {
                    return(
                        <div
                        key={index}
                        className={styles["searchResultEntity"]}
                        onMouseEnter={() => {setCurrentMangaData(manga)}}
                        onMouseLeave={() => {setCurrentMangaData(null)}}
                        >
                            <img src={manga.imageUrl.image_url}/><br/>
                            <h3>{manga.title[0] !== null ? manga.title[0] : manga.title[1]}</h3>
                            {currentMangaData === manga && (
                                <button onClick={() => addMangaData(currentMangaData)}>Add</button>
                            )}
                        </div>
                    )
                })}
            </div>
        </div>
        </>
    )
}
