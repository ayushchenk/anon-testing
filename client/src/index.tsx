import React from 'react';
import ReactDOM from 'react-dom';
import { App } from './components/App/App';
import './index.css';
import "reflect-metadata";

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);

Symbol.for("test");