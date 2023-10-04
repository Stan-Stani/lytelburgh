import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
import "./App.css";
import { CompletionClient, CompletionRequestDTO, ICompletionRequestDTO } from "./api/lytelburghApi";
import {Home} from './pages/home'

const api = new CompletionClient("https://localhost:7146");

function App() {

  return (
    <>
    <Home />
    </>
  );
}

export default App;
