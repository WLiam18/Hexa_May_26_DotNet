const baggageList = [
  {
    token: "BAG1001",
    passenger: "Arjun Kumar",
    flight: "AI-202",
    destination: "Delhi",
    weight: 18,
    status: "Checked In",
  },
  {
    token: "BAG1002",
    passenger: "Gneshwar",
    flight: "UK-809",
    destination: "Bangalore",
    weight: 16,
    status: "At Security",
  },
  {
    token: "BAG1003",
    passenger: "sarveshawar",
    flight: "6E-305",
    destination: "Mumbai",
    weight: 22,
    status: "Loaded",
  },
  {
    token: "BAG1004",
    passenger: "Afrin",
    flight: "AI-404",
    destination: "Chennai",
    weight: 20,
    status: "Checked In",
  },
];

const baggageContainer = document.getElementById("baggageContainer");
const totalBags = document.getElementById("totalBags");
const checkedInCount = document.getElementById("checkedInCount");
const securityCount = document.getElementById("securityCount");
const loadedCount = document.getElementById("loadedCount");

function getStatusClass(status) {
  if (status === "Checked In") {
    return "status-checked";
  } else if (status === "At Security") {
    return "status-security";
  } else if (status === "loaded") {
    return "status-loaded";
  }
}

function createBaggageCard(bag) {
  const card = document.createElement("div");
  card.className = "bag-card";

  const title = document.createElement("h3");
  title.textContent = `Bag Token: ${bag.token}`;

  const passenger = document.createElement("p");
  passenger.innerHTML = `<strong>Passenger: </strong> ${bag.passenger}`;

  const flight = document.createElement("p");
  flight.innerHTML = `<strong>Flight: </strong> ${bag.flight}`;

  const destination = document.createElement("p");
  destination.innerHTML = `<strong>Destination: </strong> ${bag.destination}`;

  const weight = document.createElement("p");
  weight.innerHTML = `<strong>Weight: </strong> ${bag.weight}`;

  const status = document.createElement("span");
  status.textContent = bag.status;
  status.className = `status ${getStatusClass(bag.status)}`;

  const buttonContainer = document.createElement("div");
  buttonContainer.className = "action-buttons";

  const securityButton = document.createElement("button");
  securityButton.textContent = "Move to security";
  securityButton.className = "security-btn";

  const loadedButton = document.createElement("button");
  loadedButton.textContent = "Mark loaded";
  loadedButton.className = "loaded-btn";

  securityButton.addEventListener("click", function () {
    bag.status = "At Security";
    renderBaggageCards();
  });

  loadedButton.addEventListener("click", function () {
    bag.status = "Loaded";
    renderBaggageCards();
  });

  if (bag.status === "At Security" || bag.status === "Loaded") {
    securityButton.disabled = true;
  }
  if (bag.status === "Loaded") {
    loadedButton.disabled = true;
  }
  buttonContainer.appendChild(securityButton);
  buttonContainer.appendChild(loadedButton);

  card.appendChild(title);
  card.appendChild(passenger);
  card.appendChild(flight);
  card.appendChild(destination);
  card.appendChild(weight);
  card.appendChild(status);
  card.appendChild(buttonContainer);
  return card;
}

function renderBaggageCards() {
  baggageContainer.innerHTML = "";
  baggageList.forEach(function (bag) {
    const card = createBaggageCard(bag);
    baggageContainer.appendChild(card);
  });

  updateSummary();
}

function updateSummary() {
  totalBags.textContent = baggageList.length;
  const checkedIn = baggageList.filter(function (bag) {
    return bag.status === "Checked In";
  }).length;

  const security = baggageList.filter(function (bag) {
    return bag.status === "At Security";
  }).length;

  const loaded = baggageList.filter(function (bag) {
    return bag.status === "Loaded";
  }).length;

  checkedInCount.textContent = checkedIn;
  securityCount.textContent = security;
  loadedCount.textContent = loaded;
}
renderBaggageCards();
