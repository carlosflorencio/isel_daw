import React, { Component } from "react";
import "./assets/css/App.css";

import { Provider } from "react-redux";
import configureStore from "./redux/store";

import MainContent from "./components/MainContent";

const store = configureStore();

class App extends Component {
  render() {
    return (
      <Provider store={store}>
        <MainContent />
      </Provider>
    );
  }
}

export default App;
