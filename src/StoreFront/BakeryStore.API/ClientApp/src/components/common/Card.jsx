import React from "react";
import "./Card.css";

export default function Card({ imageUrl, title, description, children }) {
  return (
    <div className="card card-container">
      <img src={imageUrl} className="card-img-top" alt="..." />
      <div className="card-body">
        <h5 className="card-title">{title}</h5>
        <p className="card-text">{description}</p>
        {children}
      </div>
    </div>
  );
}
