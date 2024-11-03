import { StrictMode, useState } from 'react';
import { createRoot } from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import './index.css';
import HomePage from '../pages/HomePage.jsx';
import SingInPage from '../pages/SignInPage.jsx';
import SignUpPage from '../pages/SignUpPage.jsx';
import NotFoundPage from '../pages/NotFoundPage.jsx';
import MainPage from '../pages/MainPage.jsx';
import { UserProvider } from './contexts/UserContext.jsx';
import LogoutPage from '../pages/LogoutPage.jsx';
import SearchMusicPage from '../pages/SearchMusicPage.jsx';
import UserPage from '../pages/UserPage.jsx';
import SearchBookPage from '../pages/SearchBookPage.jsx';
import SearchAnimePage from '../pages/SearchAnimePage.jsx';
import SearchMangaPage from '../pages/SearchMangaPage.jsx';

const router = createBrowserRouter([
  {
    path: '/',
    element: <HomePage/>,
    errorElement: <NotFoundPage/>
  },
  {
    path: 'signin',
    element: <SingInPage/>
  },
  {
    path: 'signup',
    element: <SignUpPage/>
  },
  {
    path: 'home',
    element: <MainPage/>
  },
  {
    path: 'logout',
    element: <LogoutPage/>
  },
  {
    path: 'search/music',
    element: <SearchMusicPage/>
  },
  {
    path: 'myprofile',
    element: <UserPage/>
  },
  {
    path: 'user/:username',
    element: <UserPage/>
  },
  {
    path: 'search/book',
    element: <SearchBookPage/>
  },
  {
    path: 'search/anime',
    element: <SearchAnimePage/>
  },
  {
    path: 'search/manga',
    element: <SearchMangaPage/>
  }
])

createRoot(document.getElementById('root')).render(
  <StrictMode>
    {/* <UserContext.Provider value={{isUserLoggedIn, setIsUserLoggedIn}}> */}
    <UserProvider>
      <RouterProvider router={router}/>
    </UserProvider>
    {/* </UserContext.Provider> */}
  </StrictMode>,
  // <StrictMode>
  //   <RouterProvider router={router}/>
  // </StrictMode>,
)
