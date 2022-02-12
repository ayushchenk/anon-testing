import React from 'react';
import ReactDOM from 'react-dom';
import { App } from './Components/App/App';
import './index.css';
import './flash.css';
import { BrowserRouter } from 'react-router-dom';

ReactDOM.render(
   <React.StrictMode>
      <BrowserRouter>
         <App />
      </BrowserRouter>
   </React.StrictMode>,
   document.getElementById('root')
);

Symbol.for("test");