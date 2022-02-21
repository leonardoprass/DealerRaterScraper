# Definitively not a KGB repo!

Just an innocent tool to scraple reviews for McKaig Chevrolet Buick from [DealerRater](https://www.dealerrater.com/) and return the top three most “overly positive” endorsements. 

## Criteria

In order to get the top three most positive comments, there is some rules applied:

| Data| Description | Score  |
| :------------- |:-------------| :-----|
| Dealership Rating      | 1 to 5 score of dealership | Rate * 2 |
| Average Service Rating      | Average score of service ratings      |   Rate |
| Averate Employees Rating | Average score of employees ratings      |    Rate |
| Recommended Dealer | Flag for recommending dealer      |    When recommended +5 |
| Service Type | Reason for visit      |    When Sales New Vehicle = 3, when sales old vehicle = 2, when service = 1 |
| NLP Score | Text sentiment analysis for the review content | All words are mapped to a word bag and classified as positive or negative, +1 point for each positive word found in the review comment, and -1 for each negative |

## Building 
To run the application, execute the following command in the base folder:

    dotnet run --project ./DealerRaterScraper.Client/DealerRaterScraper.Client.csproj
    
Running tests:

    dotnet test
