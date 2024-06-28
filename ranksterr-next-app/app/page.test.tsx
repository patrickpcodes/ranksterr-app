import { render, screen } from "@testing-library/react";
import Page from "./page";
// import { act } from "react";

it("App Router: Works with Server Components", async () => {
  // await act(async () => {
  render(<Page />);
  // });
  expect(screen.getByRole("heading", { level: 1 })).toHaveTextContent(
    "App Router"
  );
});
