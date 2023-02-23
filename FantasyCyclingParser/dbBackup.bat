cd\
cd\Program Files\MongoDB\Server\4.2\bin



mongodump -d FantasyCycling

mongo FantasyCycling_Dev --eval "db.dropDatabase()"

mongorestore -d FantasyCycling_Dev dump/FantasyCycling