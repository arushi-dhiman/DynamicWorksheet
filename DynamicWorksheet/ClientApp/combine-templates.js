const fs = require('fs');
const path = require('path');

const inputDir = __dirname+'/src/templates'; // Specify the directory where your JSON files are located
const outputFile = 'worksheetTemplates.json'; // Specify the output file

const combinedData = {"Templates":[]};

// Read each JSON file and combine their content
fs.readdirSync(inputDir).forEach(file => {
  if (file.endsWith('.json')) {
    const filePath = path.join(inputDir, file);
    const jsonData = JSON.parse(fs.readFileSync(filePath,'utf-8'));
    combinedData.Templates.push(jsonData);
  }
});

// Write the combined data to the output file
fs.writeFileSync(outputFile, JSON.stringify(combinedData, null, 2));

console.log('JSON files combined successfully!');
