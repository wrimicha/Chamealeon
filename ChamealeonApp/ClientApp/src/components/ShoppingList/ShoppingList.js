import React, { Fragment, useState, useEffect } from "react";
import ShoppingListItem from "../ShoppingList/ShoppingListItem/ShoppingListItem";
import "../../styles/ShoppingList.css";
import { BiPlus } from "react-icons/bi";
import axios from "axios";
import { Accordion, ListGroup } from "react-bootstrap";

export default function ShoppingList() {
  const [shoppingList, setShoppingList] = useState([]);
  const [loading, setLoading] = useState(true);

  const token = localStorage.getItem("jwt");
  const config = {
    headers: { Authorization: `Bearer ${token}` },
  };

  useEffect(() => {
    //Reference; https://stackabuse.com/making-asynchronous-http-requests-in-javascript-with-axios/
    axios
      .get("http://localhost:5000/api/shoppinglist", config)
      .then((resp) => {
        console.log(resp.data);
        setShoppingList(resp.data);
        setLoading(false);
      })
      .catch((err) => {
        console.error(err);
      });
  }, []);

  return (
    <div>
      <Accordion defaultActiveKey="0" flush>
        {loading ? (
          <p>Loading...</p>
        ) : (
          <div>
            {console.log(shoppingList)}
            {shoppingList.map((data, index) => (
              <Accordion.Item eventKey={index}>
                <Accordion.Header>{data.name}</Accordion.Header>
                <Accordion.Body>
                  <ListGroup>
                    {data.result.map((ingredients, index) => {
                      return (
                        <ListGroup.Item>
                            <ListGroup>
                                <ListGroup.Item>Amount: {ingredients.amount}</ListGroup.Item>
                                <ListGroup.Item>Unit of Measurement: {ingredients.unitOfMeasurement}</ListGroup.Item>
                            </ListGroup>
                        </ListGroup.Item>

                        // <div>
                        //     <li>{ingredients.name}</li>
                        //     <li>{ingredients.amount}</li>
                        //     <li>{ingredients.unitOfMeasurement}</li>
                        // </div>
                      );
                    })}
                  </ListGroup>
                </Accordion.Body>
              </Accordion.Item>
            ))}
          </div>
        )}
      </Accordion>
    </div>
  );
}
