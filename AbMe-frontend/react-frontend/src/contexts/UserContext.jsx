import { createContext, useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";

export const UserContext = createContext();

export function UserProvider({ children }) {
    const [userInfo, setUserInfo] = useState(null);

    useEffect(() => {
        var token = localStorage.getItem('token');
        if(token === null)
            return;

        var decodedToken = jwtDecode(token);
        setUserInfo(decodedToken);
    }, [localStorage.getItem('token')])

    return (
        <UserContext.Provider value={{userInfo, setUserInfo}}>
            { children }
        </UserContext.Provider>
    )
}