import { createContext, useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";

export const UserContext = createContext();

export function UserProvider({ children }) {
    const [userInfo, setUserInfo] = useState(null);

    return (
        <UserContext.Provider value={{userInfo, setUserInfo}}>
            { children }
        </UserContext.Provider>
    )
}