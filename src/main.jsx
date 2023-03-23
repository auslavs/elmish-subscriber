import React from 'react'
import ReactDOM from 'react-dom/client'
import { LogHistory } from '../Library.fs.js'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <LogHistory initialValue="5" />
  </React.StrictMode>,
)
