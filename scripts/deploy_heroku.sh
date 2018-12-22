docker-compose build --force-rm
docker login --username=_ --password=$api_key registry.heroku.com
docker tag jsondiffer registry.heroku.com/$heroku_name/web
docker push registry.heroku.com/$heroku_name/web