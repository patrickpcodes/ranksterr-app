import Counter from "./counter";

export const metadata = {
  title: "App Router",
};

export default function Page() {
  return (
    <div>
      <h1>App Router Test</h1>
      <Counter />
    </div>
  );
}
