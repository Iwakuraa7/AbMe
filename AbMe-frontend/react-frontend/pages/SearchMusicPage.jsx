import { useEffect, useState } from "react"

const CLIENT_ID = '779748b371464a0b85a3ea0bdcf47f39';
const CLIENT_SECRET = '0233d3de035f42be8d891773b009a1b5';

export default function SearchMusic() {
    const [searchInput, setSearchInput] = useState('');
    const [albums, setAlbums] = useState([]);
    const [tracks, setTracks] = useState([]);

    useEffect(() => {
        var authParams = {
            method: 'POST',
            headers: {
                'Content-type': 'application/x-www-form-urlencoded'
            },
            body: 'grant_type=client_credentials&client_id=' + CLIENT_ID + '&client_secret=' + CLIENT_SECRET
        }
        
        // Make async + error handling
        fetch('https://accounts.spotify.com/api/token', authParams)
            .then(result => result.json())
            .then(data => {
                const spotifyToken = data.access_token;
                // Maybe add if-else statement -> whether to update or not the token
                localStorage.setItem('spotifyToken', spotifyToken);
            });
    },  [])

    async function searchMusic() {
        var searchParams = {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('spotifyToken'),
                'Content-Type': 'application/json'
            }
        }

        // var artistId = await fetch('https://api.spotify.com/v1/search?q=' + searchInput + '&type=artist', searchParams)
        //     .then(response => response.json())
        //     .then(data => { return data.artists.items[0].id });

        // Returns an array of artists, albums, tracks by the given query
        var albums = await fetch('https://api.spotify.com/v1/search?q=' + searchInput + '&type=album,artist,track', searchParams)
            .then(response => response.json())
            .then(data => {console.log(data); setTracks(data.tracks.items); setAlbums(data.albums.items)});

        console.log(albums);
    }

    return (
        <>
        <input type='text' onChange={(e) => {setSearchInput(e.target.value)}}></input>
        <button onClick={searchMusic}>Search</button><br/>
        <h2>Albums</h2>
        <div className='searchBox'>
            {albums.map(album => {
                return(
                    <div className="searchResultEntity">
                        <img src={album.images[1].url}></img><br/>
                        <h3>{album.name}</h3>
                    </div>
                )
            })}
        </div>
        <h2>Tracks</h2>
        <div className='searchBox'>
            {tracks.map(track => {
                return(
                    <div className="searchResultEntity">
                        <img src={track.album.images[1].url}></img><br/>
                        <h3>{track.name}</h3>
                    </div>
                )
            })}
        </div>
        </>
    )
}