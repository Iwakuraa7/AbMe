import { createContext, useState } from "react";

export const UserContext = createContext();

export function UserProvider({ children }) {
    const [isUserLoggedIn, setIsUserLoggedIn] = useState(false);

    return (
        <UserContext.Provider value={{isUserLoggedIn, setIsUserLoggedIn}}>
            { children }
        </UserContext.Provider>
    )
}