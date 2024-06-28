/**
 * @jest-environment jsdom
 */
import { fireEvent, render, screen } from "@testing-library/react";
import Counter from "./counter";

it("Testing Counter Addition and Subtraction", () => {
  render(<Counter />);
  expect(screen.getByRole("heading")).toHaveTextContent("0");
  fireEvent.click(screen.getByTitle("add"));
  expect(screen.getByRole("heading")).toHaveTextContent("1");
  fireEvent.click(screen.getByTitle("add"));
  expect(screen.getByRole("heading")).toHaveTextContent("2");
  fireEvent.click(screen.getByTitle("sub"));
  expect(screen.getByRole("heading")).toHaveTextContent("1");
});
