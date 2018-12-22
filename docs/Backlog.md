# Backlog

## Post JSON doc for Diff

**As** service consumer  
**I would like to** append a document  
**In order to** compare it another document

- I should be able post a JSON base64
- I should be able to define a key as reference to the doc comparison
- I Should be able to post on 2 different pages, left and right
  - <host>/v1/diff/<ID>/left 
  - <host>/v1/diff/<ID>/right
- When a document is posted in a side, the id used shouldn't again for the same call. In that case should return a bad request(400) with an appropriate message

## Post request for JSON diff

**As** service consumer  
**I would like to** post a diff request
**In order to** compare JSONs

- To execute the action, id value should be passed
  - <host>/v1/diff/<ID>
- It will return a JSON with the results
  - If equal return that
  - If not of equal size just return that
  - If of same size provide insight in where the diffs are, actual diffs are not needed.
    - So mainly offsets + length in the data
- If there is any diff side missing, it should return a bad request(400), and a message alerting the missing sides