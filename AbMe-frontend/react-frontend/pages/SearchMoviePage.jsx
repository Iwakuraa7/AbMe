import { useContext, useEffect, useState } from "react"
import { useSearchParams } from "react-router-dom";
import NavBar from "../components/NavBar";
import styles from "../styles/SearchPage.module.css";
import { UserContext } from "../src/contexts/UserContext.jsx";

export default function SearchMoviePage() {
    const [searchInput, setSearchInput] = useState(null);
    const [mediaData, setMediaData] = useState(null);
    const [currentMedia, setCurrentMedia] = useState(null);
    const {READ_ACCESS_TOKEN} = useContext(UserContext);

    async function handleSearch() {
        var response = await fetch(`https://api.themoviedb.org/3/search/multi?query=${searchInput}&include_adult=false&language=en-US&page=1$include_images=true`, {
            method: "GET",
            headers: {
                accept: "application/json",
                Authorization: "Bearer " + READ_ACCESS_TOKEN
            }
        })
        
        var data = await response.json();
        
        console.log(data);

        setMediaData(data.results);
    }

    async function addMediaData() {
        var response = await fetch("http://localhost:5078/api/movie/create", {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Title: currentMedia.name ? currentMedia.name : currentMedia.title,
                ImageUrl: `https://image.tmdb.org/t/p/original${currentMedia.poster_path}`
            })
        })

        var data = await response.json();

        if(data.succeeded)
            console.log("Successfully created new media data");
        else
            console.log(data.message);
    }

    return (
        <>
        <NavBar/>
        <div className={styles["search-main-box"]}>
            <div className={styles["search-input-box"]}>
                <h1>Add movie or show to profile</h1>
                <div className={styles["search-and-button-rel"]}>
                    <input type='text' onChange={(e) => {setSearchInput(e.target.value)}}></input>
                    <button onClick={handleSearch}>Search</button><br/>
                </div>
            </div>

            <div className={styles["searchBox"]}>
                {mediaData && mediaData.map((media, index) => {
                    const mediaImage = `https://image.tmdb.org/t/p/original${media.poster_path}`;

                    return(
                        <div
                        key={index}
                        className={styles["searchResultEntity"]}
                        onMouseEnter={() => {setCurrentMedia(media)}}
                        onMouseLeave={() => {setCurrentMedia(null)}}
                        >
                            <img src={mediaImage} style={{width: "300px", height: "450px"}}/><br/>
                            <h3>{media.name ? media.name : media.title}</h3>
                            {currentMedia === media && (
                                <button onClick={() => addMediaData(currentMedia)}>Add</button>
                            )}
                        </div>
                    )
                })}
            </div>
        </div>
        </>
    )
}