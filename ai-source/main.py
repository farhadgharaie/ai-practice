from fastapi import FastAPI

app = FastAPI()


@app.get("/")
def read_root():
    return {"message": "Hello, World from FastAPI!"}


@app.get("/company/search/{name}")
def read_item(name: str | None = None):
    return [{"name": name, "id": "122"}]
