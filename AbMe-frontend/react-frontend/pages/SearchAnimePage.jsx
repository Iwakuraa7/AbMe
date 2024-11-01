import { useEffect, useState } from "react";
import styles from "../styles/SearchPage.module.css";
import NavBar from "../components/NavBar";

const CLIENT_ID = '5cffb785e93032f680689d03f0f8819d';

export default function SearchAnimePage() {
    const [searchInput, setSearchInput] = useState(null);
    const [currentAnimeData, setCurrentAnimeData] = useState(null);
    const [animeData, setAnimeData] = useState(null);

    async function handleSearch() {
        var response = await fetch(`https://api.jikan.moe/v4/anime?q=${searchInput}`);
        var data = await response.json();

        var filteredData = data.data.map(anime => ({
            title: [anime.title_english, anime.title],
            imageUrl: anime.images.jpg
        }))

        setAnimeData(filteredData);

        console.log(filteredData);
        console.log(data.data);
    }

    async function addAnimeData () {
        
    }

    return (
        <>
        <NavBar/>
        <div className={styles["search-main-box"]}>
            <div className={styles["search-input-box"]}>
                <h1>Add anime to profile</h1>
                <div className={styles["search-and-button-rel"]}>
                    <input type='text' onChange={(e) => {setSearchInput(e.target.value)}}></input>
                    <button onClick={handleSearch}>Search</button><br/>
                </div>
            </div>

            <div className={styles["center-text-box"]}>
                {animeData && <h2>Anime</h2>}
            </div>

            <div className={styles["searchBox"]}>
                {animeData && animeData.map((anime, index) => {
                    return(
                        <div
                        key={index}
                        className={styles["searchResultEntity"]}
                        onMouseEnter={() => {setCurrentAnimeData(anime)}}
                        onMouseLeave={() => {setCurrentAnimeData(null)}}
                        >
                            <img src={anime.imageUrl.image_url}/><br/>
                            <h3>{anime.title[0] !== null ? anime.title[0] : anime.title[1]}</h3>
                            {currentAnimeData === anime && (
                                <button onClick={() => addAnimeData(currentAnimeData)}>Add</button>
                            )}
                        </div>
                    )
                })}
            </div>
        </div>
        </>
    )
}
